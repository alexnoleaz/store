import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';

import { Product } from '../../product.model';
import { ProductService } from '../../product.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-list',
  imports: [CommonModule, RouterModule],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css',
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  page = 1;
  pageSize = 10;

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute,
    private router: Router,
  ) {}

  ngOnInit() {
    this.route.queryParams.subscribe((params) => {
      this.page = +params['page'] || 1;
      this.pageSize = +params['pageSize'] || 10;
      this.loadProducts();
    });
  }

  loadProducts() {
    this.productService
      .findAll({ page: this.page, pageSize: this.pageSize })
      .subscribe((data) => {
        this.products = data;
      });
  }

  delete(id: number) {
    this.productService.delete(id).subscribe(() => this.loadProducts());
  }

  changePage(delta: number) {
    const newPage = this.page + delta;
    if (newPage >= 1) {
      this.router.navigate([], {
        queryParams: { page: newPage, pageSize: this.pageSize },
        queryParamsHandling: 'merge',
      });
    }
  }
}
