import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Product } from '../../product.model';
import { ProductService } from '../../product.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-form',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './product-form.component.html',
  styleUrl: './product-form.component.css',
})
export class ProductFormComponent implements OnInit {
  form: FormGroup;
  id?: number;

  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private route: ActivatedRoute,
    private router: Router,
  ) {
    this.form = this.fb.group({
      name: [''],
      sku: [''],
      price: [0],
      stock: [0],
    });
  }

  ngOnInit() {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    if (this.id) {
      this.productService
        .findById(this.id)
        .subscribe((product) => this.form.patchValue(product));
    }
  }

  save() {
    const product: Product = { id: this.id!, ...this.form.value };
    const req = this.id
      ? this.productService.update(product)
      : this.productService.insert(this.form.value);

    req.subscribe(() => this.router.navigate(['/products']));
  }
}
