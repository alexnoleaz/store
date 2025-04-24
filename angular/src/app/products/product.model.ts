import { ProductStatus } from './product-status';

export interface Product {
  id: number;
  sku: string;
  name: string;
  stock: number;
  status: ProductStatus;
  price: number;
}
