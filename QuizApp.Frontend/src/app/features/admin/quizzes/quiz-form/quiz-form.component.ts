import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-quiz-form',
  standalone: true,
  imports: [RouterLink],
  template: `
    <div class="quiz-form">
      <div class="header">
        <h2>Quiz Form</h2>
        <a routerLink="../" class="btn btn-secondary">Back to List</a>
      </div>
      <div class="form">
        <!-- Quiz form will be implemented here -->
        <p>Quiz form coming soon...</p>
      </div>
    </div>
  `,
  styles: [`
    .quiz-form {
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
    .btn-secondary {
      background-color: #6c757d;
      color: white;
    }
  `]
})
export class QuizFormComponent {} 