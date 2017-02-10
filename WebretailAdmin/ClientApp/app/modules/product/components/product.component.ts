import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TreeNode, Message, MenuItem } from 'primeng/primeng';
import { Observable } from 'rxjs/Rx';
import { AuthenticationService } from './../../../services/authentication.service';
import { ProductService } from './../../../services/product.service';
import { Product, Article, ArticleAttributeValue, AttributeValue } from './../../../shared/models';
import { Helpers } from './../../../shared/helpers';

@Component({
    selector: 'product',
    templateUrl: 'product.component.html'
})

export class ProductComponent implements OnInit {

    private sub: any;
    msgs: Message[] = [];
    items: MenuItem[];
    product: Product;
    header: string[];
    articles: any[];
    totalRecords = 0;
    selected: any;
    productInfo: TreeNode[];
    selectedNode: TreeNode;

	constructor(private authenticationService: AuthenticationService,
                private activatedRoute: ActivatedRoute,
                private productService: ProductService) {}

    ngOnInit() {
        this.authenticationService.checkCredentials();

        // Subscribe to route params
        this.sub = this.activatedRoute.params.subscribe(params => {
            let id = params['id'];
            this.productService.getProduct(id).subscribe(result => {
                this.product = result;
                this.createTree();
                this.createSheet();
            });
        });

        this.items = [
            {label: 'Remove', icon: 'fa-trash', command: () => {
                this.delete();
            }},
            {label: 'Build Articles', icon: 'fa-database', command: () => {
                this.save();
            }}
        ];
    }

    ngOnDestroy() {
        // Clean sub to avoid memory leak
        this.sub.unsubscribe();
    }

    newNode(label: string, data: string, type: string) : TreeNode {
        return <TreeNode>{
                'label': label,
                'data': data,
                'type': type,
                'children': []
        };
    }

    createTree() {
        let rootNode = this.newNode(this.product.productName, this.product.productCode, 'product');
        rootNode.expanded = true;

        let producerNode = this.newNode('Brand', '[]', 'brand');
        producerNode.expanded = true;
        producerNode.children.push(this.newNode(this.product.brand.brandName, this.product.brand.brandId.toString(), 'brand'));
        rootNode.children.push(producerNode);

        let categoriesNode = this.newNode('Categories', '[]', 'category');
        this.product.categories.forEach(elem => {
            categoriesNode.children.push(this.newNode(elem.category.categoryName, elem.category.categoryId.toString(), 'category'));
        });
        categoriesNode.expanded = categoriesNode.children.length > 0;
        rootNode.children.push(categoriesNode);

        let attributesNode = this.newNode('Attributes', '[]', 'attribute');
        attributesNode.expanded = true;
        this.product.attributes.forEach(elem => {
            let node = this.newNode(elem.attribute.attributeName, elem.attribute.attributeId.toString(), 'attribute');
            this.product.attributeValues.filter(p => p.attributeValue.attributeId == elem.attribute.attributeId).forEach(e =>
                node.children.push(this.newNode(e.attributeValue.attributeValueName, e.attributeValue.attributeValueId.toString(), 'attributeValue'))
            );
            node.expanded = node.children.length > 0;
            attributesNode.children.push(node);
        });
        attributesNode.expanded = attributesNode.children.length > 0;
        rootNode.children.push(attributesNode);

        this.productInfo = [rootNode];
    }

    createSheet() {
        this.totalRecords = this.product.articles.length;
        this.header = [];
        this.articles = [];

        let lenght = this.product.attributes.length - 1;
        if (lenght > 0) {
            this.product.attributes.forEach(elem => {
                this.header.push(elem.attribute.attributeName);
            });
            this.header.pop();

            let attributeId = this.product.attributes[lenght].attribute.attributeId;
            this.product.attributeValues.forEach(elem => {
                if (elem.attributeValue.attributeId === attributeId) {
                    this.header.push(elem.attributeValue.attributeValueName);
                }
            });
        }

        let source = Observable.from(this.product.articles)
            .groupBy(
                function (x) {
                    return x.attributeValues
                        .map(p => p.attributeValue.attributeValueName)
                        .slice(0, x.attributeValues.length - 2)
                        .join('#');
                },
                function (x) { return x; }
            );

        source.subscribe(obs => {
            let row: any[] = [];
            console.log('Key: ' + obs.key);
            let isFirst = true;
            obs.forEach(e => {
                //TODO: get stock for this barcode
                let qta = 1;
                console.log('Obs: ' + e.barcode);
                if (isFirst) {
                    e.attributeValues.forEach(ex => {
                        console.log('Obs: ' + ex.attributeValue.attributeValueName);
                        row.push(ex.attributeValue.attributeValueName);
                    });
                    isFirst = false;
                    row[row.length - 1] = qta;
                } else {
                    row.push(qta);
                    //row.push(e.attributeValues[e.attributeValues.length - 1].attributeValue.attributeValueName);
                }
            }).then(p => {
                this.articles.push(row);
            });
        }, err => console.log('Error: ' + err));
    }

    save() {
        this.msgs = [];
        this.msgs.push({severity: 'info', summary: 'Success', detail: 'Data Saved'});
    }

    add() {
        this.msgs = [];
        this.msgs.push({severity: 'info', summary: 'Success', detail: 'Data Added'});
    }

    delete() {
        this.msgs = [];
        this.msgs.push({severity: 'info', summary: 'Success', detail: 'Data Deleted'});
    }
}
