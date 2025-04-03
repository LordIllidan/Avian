import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { QuizService } from '../../../../services/quiz.service';

@Component({
  selector: 'app-quiz-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <div class="quiz-form-container">
      <mat-card>
        <mat-card-header>
          <mat-card-title>Create New Quiz</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <form [formGroup]="quizForm" (ngSubmit)="onSubmit()">
            <div class="form-group">
              <mat-form-field appearance="outline">
                <mat-label>Quiz Title</mat-label>
                <input matInput formControlName="title" placeholder="Enter quiz title">
                <mat-error *ngIf="quizForm.get('title')?.hasError('required')">
                  Title is required
                </mat-error>
              </mat-form-field>
            </div>
            
            <div class="form-group">
              <mat-form-field appearance="outline">
                <mat-label>Description</mat-label>
                <textarea matInput formControlName="description" placeholder="Enter quiz description" rows="3"></textarea>
                <mat-error *ngIf="quizForm.get('description')?.hasError('required')">
                  Description is required
                </mat-error>
              </mat-form-field>
            </div>

            <div class="form-group">
              <mat-form-field appearance="outline">
                <mat-label>Time Limit (minutes)</mat-label>
                <input matInput type="number" formControlName="timeLimit" placeholder="Enter time limit in minutes">
                <mat-error *ngIf="quizForm.get('timeLimit')?.hasError('required')">
                  Time limit is required
                </mat-error>
                <mat-error *ngIf="quizForm.get('timeLimit')?.hasError('min')">
                  Time limit must be greater than 0
                </mat-error>
              </mat-form-field>
            </div>

            <div class="form-group">
              <mat-form-field appearance="outline">
                <mat-label>Expiration Date</mat-label>
                <input matInput [matDatepicker]="picker" formControlName="expiresAt">
                <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
                <mat-datepicker #picker></mat-datepicker>
              </mat-form-field>
            </div>
            
            <div class="form-actions">
              <button mat-button type="button" (click)="goBack()">Cancel</button>
              <button mat-raised-button color="primary" type="submit" [disabled]="quizForm.invalid">
                Create Quiz
              </button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .quiz-form-container {
      max-width: 800px;
      margin: 0 auto;
      padding: 2rem;
    }
    
    .form-group {
      margin-bottom: 1.5rem;
    }
    
    mat-form-field {
      width: 100%;
    }
    
    .form-actions {
      display: flex;
      justify-content: flex-end;
      gap: 1rem;
      margin-top: 2rem;
    }
  `]
})
export class QuizFormComponent {
  quizForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private quizService: QuizService
  ) {
    this.quizForm = this.fb.group({
      title: ['', [Validators.required]],
      description: ['', [Validators.required]],
      timeLimit: [30, [Validators.required, Validators.min(1)]],
      expiresAt: [null]
    });
  }

  onSubmit(): void {
    if (this.quizForm.valid) {
      const formValue = this.quizForm.value;
      
      // Convert the form data to match the backend model
      const quizData = {
        title: formValue.title,
        description: formValue.description,
        timeLimit: formValue.timeLimit,
        expiresAt: formValue.expiresAt ? new Date(formValue.expiresAt).toISOString() : null,
        questions: [] // Add empty questions array to match the Quiz interface
      };
      
      this.quizService.createQuiz(quizData).subscribe({
        next: (response) => {
          console.log('Quiz created:', response);
          this.router.navigate(['/quizzes']);
        },
        error: (error) => {
          console.error('Error creating quiz:', error);
        }
      });
    }
  }

  goBack(): void {
    this.router.navigate(['/quizzes']);
  }
} 