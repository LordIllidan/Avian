import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { NgIf } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, RouterOutlet, NgIf, MatIconModule],
  template: `
    <div class="layout">
      <nav class="navbar">
        <div class="navbar-brand">
          <a routerLink="/quizzes">Quiz App</a>
        </div>
        <div class="navbar-menu">
          <a routerLink="/quizzes" routerLinkActive="active">
            <mat-icon>list</mat-icon>
            My Quizzes
          </a>
          <a routerLink="/quizzes/new" routerLinkActive="active">
            <mat-icon>add</mat-icon>
            New Quiz
          </a>
          <ng-container *ngIf="isAdmin">
            <a routerLink="/admin/quizzes" routerLinkActive="active">
              <mat-icon>edit</mat-icon>
              Manage Quizzes
            </a>
            <a routerLink="/admin/users" routerLinkActive="active">
              <mat-icon>people</mat-icon>
              Manage Users
            </a>
          </ng-container>
        </div>
        <div class="navbar-end">
          <span class="user-info">{{ username }}</span>
          <button (click)="logout()" class="btn btn-outline">Logout</button>
        </div>
      </nav>
      <main class="main-content">
        <router-outlet></router-outlet>
      </main>
    </div>
  `,
  styles: [`
    .layout {
      min-height: 100vh;
      display: flex;
      flex-direction: column;
    }
    .navbar {
      background-color: #2c3e50;
      padding: 1rem;
      display: flex;
      justify-content: space-between;
      align-items: center;
      color: white;
    }
    .navbar-brand a {
      color: white;
      text-decoration: none;
      font-size: 1.5rem;
      font-weight: bold;
    }
    .navbar-menu {
      display: flex;
      gap: 1rem;
    }
    .navbar-menu a {
      color: #ecf0f1;
      text-decoration: none;
      padding: 0.5rem 1rem;
      border-radius: 4px;
      transition: background-color 0.2s;
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }
    .navbar-menu a:hover {
      background-color: #34495e;
    }
    .navbar-menu a.active {
      background-color: #3498db;
    }
    .navbar-end {
      display: flex;
      align-items: center;
      gap: 1rem;
    }
    .user-info {
      color: #ecf0f1;
    }
    .btn {
      padding: 0.5rem 1rem;
      border-radius: 4px;
      cursor: pointer;
      border: none;
    }
    .btn-outline {
      background-color: transparent;
      border: 1px solid #ecf0f1;
      color: #ecf0f1;
    }
    .btn-outline:hover {
      background-color: #ecf0f1;
      color: #2c3e50;
    }
    .main-content {
      flex: 1;
      padding: 2rem;
      background-color: #f5f6fa;
    }
  `]
})
export class MainLayoutComponent {
  username: string;
  isAdmin: boolean;

  constructor(private authService: AuthService) {
    const user = this.authService.getCurrentUser();
    this.username = user?.username || '';
    this.isAdmin = user?.role === 'Admin';
  }

  logout() {
    this.authService.logout();
  }
} 