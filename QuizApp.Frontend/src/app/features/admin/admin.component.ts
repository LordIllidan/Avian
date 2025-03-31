import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-admin',
  template: `
    <div class="admin-container">
      <nav class="admin-nav">
        <a routerLink="quizzes" routerLinkActive="active">Quizzes</a>
        <a routerLink="users" routerLinkActive="active">Users</a>
      </nav>
      <div class="admin-content">
        <router-outlet></router-outlet>
      </div>
    </div>
  `,
  styles: [`
    .admin-container {
      display: flex;
      height: 100%;
    }
    .admin-nav {
      width: 200px;
      padding: 1rem;
      background-color: #f5f5f5;
      border-right: 1px solid #ddd;
    }
    .admin-nav a {
      display: block;
      padding: 0.5rem;
      margin-bottom: 0.5rem;
      text-decoration: none;
      color: #333;
      border-radius: 4px;
    }
    .admin-nav a:hover {
      background-color: #e0e0e0;
    }
    .admin-nav a.active {
      background-color: #007bff;
      color: white;
    }
    .admin-content {
      flex: 1;
      padding: 1rem;
    }
  `],
  imports: [
    CommonModule,
    RouterModule,
    MatSidenavModule,
    MatListModule,
    MatIconModule,
  ]
})
export class AdminComponent {} 