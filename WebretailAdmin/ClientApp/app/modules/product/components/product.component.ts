import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TreeNode, Message, MenuItem } from 'primeng/primeng';
import { Observable } from 'rxjs/Rx';
import { AuthenticationService } from './../../../services/authentication.service';
import { Product, ProductCategory, Category, ProductAttribute, Attribute, ProductAttributeValue, Article, ArticleAttributeValue, AttributeValue } from './../../../shared/models';
import { FooterComponent } from './../../shared/components/footer/footer.component';
import { Helpers } from './../../../shared/helpers';
import { ProductService } from './../../../services/product.service';
import { CategoryService } from './../../../services/category.service';
import { AttributeService } from './../../../services/attribute.service';

@Component({
    selector: 'product',
    templateUrl: 'product.component.html',
    providers: [ CategoryService, AttributeService ]
})

export class ProductComponent implements OnInit {

    private sub: any;
    msgs: Message[] = [];
    buttons: MenuItem[];
    product: Product;
    header: string[];
    articles: any[];
    totalRecords = 0;
    selected: any;
    productInfo: TreeNode[];
    selectedNode: TreeNode;
    list1: TreeNode[];
    list2: TreeNode[];
    display: boolean;

	constructor(private authenticationService: AuthenticationService,
                private activatedRoute: ActivatedRoute,
                private productService: ProductService,
                private categoryService: CategoryService,
                private attributeService: AttributeService) {}

    ngOnInit() {
        this.authenticationService.checkCredentials();

        // Subscribe to route params
        this.sub = this.activatedRoute.params.subscribe(params => {
            let id = params['id'];
            this.productService.getProduct(id).subscribe(result => {
                this.product = result;
                this.createTree();
                this.createSheet();
            }, onerror => alert('ERROR: ' + onerror));
        });

        this.buttons = [
            {label: 'Build Articles', icon: 'fa-database', command: () => {
                this.buildClick();
            }}
        ];
    }

    ngOnDestroy() {
        // Clean sub to avoid memory leak
        this.sub.unsubscribe();
    }

    createTree() {
        let rootNode = Helpers.newNode(this.product.productName, this.product.productCode, 'product');
        rootNode.expanded = true;

        let producerNode = Helpers.newNode('Brand', '[]', 'brands');
        producerNode.expanded = true;
        producerNode.children.push(Helpers.newNode(this.product.brand.brandName, this.product.brand.brandId.toString(), 'brand'));
        rootNode.children.push(producerNode);

        let categoriesNode = Helpers.newNode('Categories', '[]', 'categories');
        this.product.categories.forEach(elem => {
            categoriesNode.children.push(Helpers.newNode(elem.category.categoryName, elem.category.categoryId.toString(), 'category'));
        });
        categoriesNode.expanded = categoriesNode.children.length > 0;
        rootNode.children.push(categoriesNode);

        let attributesNode = Helpers.newNode('Attributes', '[]', 'attributes');
        attributesNode.expanded = true;
        this.product.attributes.forEach(elem => {
            let node = Helpers.newNode(elem.attribute.attributeName, elem.attribute.attributeId.toString(), 'attribute');
            this.product.attributeValues.filter(p => p.attributeValue.attributeId == elem.attribute.attributeId).forEach(e =>
                node.children.push(Helpers.newNode(e.attributeValue.attributeValueName, e.attributeValue.attributeValueId.toString(), 'attributeValue'))
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

    editClick() {
        if (!this.selectedNode) {
            this.msgs.push({severity: 'warn', summary: 'Warning', detail: 'A node must be selected before editing!'});
            return;
        }

        this.list1 = [];

        switch (this.selectedNode.type) {
            case 'categories':
                this.list2 = this.productInfo[0].children.find(p => p.type == 'categories').children;
                this.categoryService.getAll()
                    .subscribe(result => {
                        result.forEach(p => {
                            if (this.list2.findIndex(e => e.data == p.categoryId) < 0) {
                                this.list1.push(Helpers.newNode(p.categoryName, p.categoryId.toString(), 'category'));
                            }
                        });
                    });
                break;
            case 'attributes':
                this.list2 = this.productInfo[0].children.find(p => p.type == 'attributes').children;
                this.attributeService.getAll()
                    .subscribe(result => {
                        result.forEach(p => {
                            if (this.list2.findIndex(e => e.data == p.attributeId) < 0) {
                                this.list1.push(Helpers.newNode(p.attributeName, p.attributeId.toString(), 'attribute'));
                            }
                        });
                    });
                break;
            case 'attribute':
                this.list2 = this.productInfo[0].children.find(p => p.type == 'attributes')
                                                .children.find(p => p.data == this.selectedNode.data)
                                                .children;
                this.attributeService.getValueByAttributeId(this.selectedNode.data)
                    .subscribe(result => {
                        result.forEach(p => {
                            if (this.list2.findIndex(e => e.data == p.attributeValueId) < 0) {
                                this.list1.push(Helpers.newNode(p.attributeValueName, p.attributeValueId.toString(), 'attributeValue'));
                            }
                        });
                    });
                break;
            default:
                this.msgs.push({severity: 'warn', summary: 'warning', detail: 'You can not update anything to this node!'});
                return;
        }

        this.display = true;
    }

    addNodes(event: any) {
        let nodes: TreeNode[] = event.items;
        nodes.forEach(p => {
            switch (p.type) {
                case 'category':
                    let productCategory = <ProductCategory>{
                        productId: this.product.productId,
                        category: new Category(p.data, p.label)
                    };
                    this.productService
                        .addCategory(productCategory)
                        .subscribe(result => this.msgs.push({
                                severity: 'success',
                                summary: 'Success',
                                detail: 'Added category ' + p.label
                            }));
                    break;
                case 'attribute':
                    let productAttribute = <ProductAttribute>{
                        productId: this.product.productId,
                        attribute: new Attribute(p.data, p.label, new Date())
                    };
                    this.productService
                        .addAttribute(productAttribute)
                        .subscribe(result => this.msgs.push({
                                severity: 'success',
                                summary: 'Success',
                                detail: 'Added attribute ' + p.label
                            }));
                    break;
                case 'attributeValue':
                    let productAttributeValue = <ProductAttributeValue>{
                        productId: this.product.productId,
                        attributeValue: new AttributeValue(this.selectedNode.data, p.data, p.data, p.label, new Date())
                    };
                    this.productService
                        .addAttributeValue(productAttributeValue)
                        .subscribe(result => this.msgs.push({
                                severity: 'success',
                                summary: 'Success',
                                detail: 'Added attribute value ' + p.label
                            }));
                    break;
            }
        });
    }

    removeNodes(event: any) {
        let nodes: TreeNode[] = event.items;
        nodes.forEach(p => {
            switch (p.type) {
                case 'category':
                    let productCategory = <ProductCategory>{
                        productId: this.product.productId,
                        category: new Category(p.data, p.label)
                    };
                    this.productService
                        .removeCategory(productCategory)
                        .subscribe(result => this.msgs.push({
                                severity: 'success',
                                summary: 'Success',
                                detail: 'Removed category ' + p.label
                            }));
                    break;
                case 'attribute':
                    let productAttribute = <ProductAttribute>{
                        productId: this.product.productId,
                        attribute: new Attribute(p.data, p.label, new Date())
                    };
                    this.productService
                        .removeAttribute(productAttribute)
                        .subscribe(result => this.msgs.push({
                                severity: 'success',
                                summary: 'Success',
                                detail: 'Removed attribute ' + p.label
                            }));
                    break;
                case 'attributeValue':
                    let productAttributeValue = <ProductAttributeValue>{
                        productId: this.product.productId,
                        attributeValue: new AttributeValue(this.selectedNode.data, p.data, p.data, p.label, new Date())
                    };
                    this.productService
                        .removeAttributeValue(productAttributeValue)
                        .subscribe(result => this.msgs.push({
                                severity: 'success',
                                summary: 'Success',
                                detail: 'Removed attribute value ' + p.label
                            }));
                    break;
            }
        });
    }

    buildClick() {
        this.msgs.push({severity: 'success', summary: 'Success', detail: 'Data builded'});
    }
}
