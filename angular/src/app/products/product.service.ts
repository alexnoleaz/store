import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

import { environment } from '../../environments/environment';
import { QueryParams } from '../shared/query-params.model';
import { Product } from './product.model';
import { Response } from '../shared/response.model';
import { ProductStatus } from './product-status';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private readonly apiUrl = environment.apiUrl + '/api/products';
  private readonly httpClient: HttpClient;

  constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
  }

  findAll({ page, pageSize }: QueryParams): Observable<Product[]> {
    const params = new HttpParams().set('page', page).set('pageSize', pageSize);
    return this.httpClient
      .get<Response<Product>>(this.apiUrl, { params })
      .pipe(map((res) => res.items));
  }

  findById(id: number): Observable<Product> {
    return this.httpClient.get<Product>(`${this.apiUrl}/${id}`);
  }

  insert(product: Product): Observable<Product> {
    product.status = ProductStatus.Active;
    return this.httpClient.post<Product>(this.apiUrl, product);
  }

  update(product: Product): Observable<Product | undefined> {
    return this.httpClient.put<Product>(
      `${this.apiUrl}/${product.id}`,
      product,
    );
  }

  delete(id: number): Observable<void> {
    return this.httpClient.delete<void>(`${this.apiUrl}/${id}`);
  }
}
