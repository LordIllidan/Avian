import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-quizzes-list',
  standalone: true,
  imports: [RouterLink],
  template: `
    <div class="quizzes-list">
      <div class="header">
        <h2>Quizzes</h2>
        <a routerLink="new" class="btn btn-primary">New Quiz</a>
      </div>
      <div class="list">
        <!-- Quiz list will be implemented here -->
        <p>Quiz list coming soon...</p>
      </div>
    </div>
  `,
  styles: [`
    .quizzes-list {
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
export class QuizzesListComponent {} 