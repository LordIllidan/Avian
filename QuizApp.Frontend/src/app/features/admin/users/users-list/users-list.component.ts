import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-users-list',
  standalone: true,
  imports: [RouterLink],
  template: `
    <div class="users-list">
      <div class="header">
        <h2>Users</h2>
        <a routerLink="new" class="btn btn-primary">New User</a>
      </div>
      <div class="list">
        <!-- Users list will be implemented here -->
        <p>Users list coming soon...</p>
      </div>
    </div>
  `,
  styles: [`
    .users-list {
      padding: 1rem;
    }
    .header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 1rem;
    }
    .btn {
      padding: 0.5rem 1rem;
      border-radius: 4px;
      text-decoration: none;
    }
    .btn-primary {
      background-color: #007bff;
      color: white;
    }
  `]
})
export class UsersListComponent {} 