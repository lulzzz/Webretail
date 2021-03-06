﻿import { Component, OnInit, Input } from '@angular/core';
import { Validators, FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { AuthenticationService } from './../../../services/authentication.service';
import { AttributeService } from './../../../services/attribute.service';
import { Attribute, AttributeValue } from './../../../shared/models';
import { Helpers } from './../../../shared/helpers';

@Component({
    selector: 'attribute-component',
    templateUrl: 'attribute.component.html'
})

export class AttributeComponent implements OnInit {
    totalRecords = 0;
    totalValues = 0;
    attributes: Attribute[];
    values: AttributeValue[];
	selected: Attribute;
    selectedValue: AttributeValue;
    displayDialog: boolean;
	displayDialogValue: boolean;
	dataform: FormGroup;
    dataformValue: FormGroup;

    constructor(private authenticationService: AuthenticationService,
                private attributeService: AttributeService,
                private fb: FormBuilder) { }

	ngOnInit() {
        this.authenticationService.checkCredentials();

        this.dataform = this.fb.group({
            'name': new FormControl('', Validators.required)
        });

        this.dataformValue = this.fb.group({
            'code': new FormControl('', [Validators.required, Validators.maxLength(6)]),
            'name': new FormControl('', Validators.required)
        });

        this.attributeService.getAll().subscribe(result => {
            this.attributes = result;
            this.totalRecords = this.attributes.length;
        }, onerror => alert('ERROR\r\n' + onerror));
    }

    get isNew() : boolean { return this.selected == null || this.selected.attributeId == 0; }

    get isNewValue() : boolean { return this.selectedValue == null || this.selectedValue.attributeValueId == 0; }

    get selectedIndex(): number { return this.attributes.indexOf(this.selected); }

    get selectedValueIndex(): number { return this.values.indexOf(this.selectedValue); }

    onRowSelect(event: any) {
        this.attributeService.getValueByAttributeId(this.selected.attributeId)
            .subscribe(result => {
                this.values = result;
                this.totalValues = this.values.length;
            });
    }

    addClick() {
        this.selected = new Attribute(0, '', new Date());
        this.displayDialog = true;
    }

    editClick(item: Attribute) {
        this.selected = item;
        this.displayDialog = true;
    }

    saveClick() {
        if (this.isNew) {
            this.attributeService.create(this.selected).subscribe(result => {
                this.attributes.push(result);
            });
        } else {
            this.attributeService.update(this.selected.attributeId, this.selected).subscribe(result => {
                this.attributes[this.selectedIndex] = this.selected;
            });
        }
        this.displayDialog = false;
    }

    deleteClick() {
        this.attributeService.delete(this.selected.attributeId).subscribe(result => {
            this.attributes.splice(this.selectedIndex, 1);
            this.selected = null;
            this.values.length = 0;
        });
        this.displayDialog = false;
    }

    addValueClick() {
        if (this.selected) {
            this.selectedValue = new AttributeValue(this.selected.attributeId, 0, '', '', new Date());
            this.displayDialogValue = true;
        } else {
            alert('Select a attribute before add value!');
        }
    }

    editValueClick(item: AttributeValue) {
        this.selectedValue = item;
        this.displayDialogValue = true;
    }

    saveValueClick() {
        if (this.isNewValue) {
            this.attributeService.createValue(this.selectedValue)
                .subscribe(result => {
                    this.values.push(result);
                });
        } else {
            this.attributeService.updateValue(this.selectedValue.attributeValueId, this.selectedValue)
                .subscribe(result => {
                    this.values[this.selectedValueIndex] = this.selectedValue;
                });
        }
        this.displayDialogValue = false;
    }

    deleteValueClick() {
        this.attributeService.deleteValue(this.selectedValue.attributeValueId)
            .subscribe(result => {
                this.values.splice(this.selectedValueIndex, 1);
                this.selectedValue = null;
            });
        this.displayDialogValue = false;
    }
}
