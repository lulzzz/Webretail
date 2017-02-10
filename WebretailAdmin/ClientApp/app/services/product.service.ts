import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/map';
import { Product } from '../shared/models';
import { Helpers } from '../shared/helpers';

@Injectable()
export class ProductService {

    constructor(private http: Http) {
    }

    getProducts() : Observable<Product[]> {
        return this.http.get('api/product').map(result => <Product[]>result.json());
    }

    getProduct(id: number) : Observable<Product> {
        return this.http.get('/api/product/' + id).map(result => <Product>result.json());
    }

    create(model: Product) : Observable<Product> {
        return this.http.post('/api/product', model, { headers: Helpers.getHeaders() }).map(result => <Product>result.json());
    }

    update(id: number, model: Product) : Observable<any> {
        return this.http.put('/api/product/' + id, model, { headers: Helpers.getHeaders() }).map(result => result.json());
    }

    delete(id: number) : Observable<any> {
        return this.http.delete('/api/product/' + id, { headers: Helpers.getHeaders() }).map(result => result.json());
    }
}
