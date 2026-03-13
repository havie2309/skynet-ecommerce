import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ProductService } from '../core/services/product';
import { Product } from '../models/product';

@Component({
  selector: 'app-admin-product-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './admin-product-form.html'
})
export class AdminProductForm implements OnInit {
  isEditMode = false;
  productId = 0;
  loading = false;
  error = '';

  form;

  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.form = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      price: [0, [Validators.required, Validators.min(0.01)]],
      stockQuantity: [0, [Validators.required, Validators.min(0)]],
      imageUrl: ['']
    });
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');

    if (id) {
      this.isEditMode = true;
      this.productId = +id;
      this.loadProduct();
    }
  }

  loadProduct(): void {
    this.loading = true;
    this.error = '';

    this.productService.getProduct(this.productId).subscribe({
      next: product => {
        this.form.patchValue({
          name: product.name,
          description: product.description,
          price: product.price,
          stockQuantity: product.stockQuantity,
          imageUrl: product.imageUrl ?? ''
        });
        this.loading = false;
      },
      error: () => {
        this.error = 'Failed to load product';
        this.loading = false;
      }
    });
  }

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const product: Product = {
      id: this.productId,
      name: this.form.value.name!,
      description: this.form.value.description!,
      price: Number(this.form.value.price),
      stockQuantity: Number(this.form.value.stockQuantity),
      imageUrl: this.form.value.imageUrl || null
    };

    this.loading = true;
    this.error = '';

    const request = this.isEditMode
      ? this.productService.updateProduct(this.productId, product)
      : this.productService.createProduct(product);

    request.subscribe({
      next: () => {
        this.router.navigate(['/admin/products']);
      },
      error: () => {
        this.error = this.isEditMode
          ? 'Failed to update product'
          : 'Failed to create product';
        this.loading = false;
      }
    });
  }
}
