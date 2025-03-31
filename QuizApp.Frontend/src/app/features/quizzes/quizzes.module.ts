import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatRadioModule } from '@angular/material/radio';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatIconModule } from '@angular/material/icon';

import { QuizListComponent } from './components/quiz-list/quiz-list.component';
import { QuizAttemptComponent } from './components/quiz-attempt/quiz-attempt.component';
import { QUIZ_ROUTES } from './quizzes.routes';

@NgModule({
  declarations: [
    QuizListComponent,
    QuizAttemptComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(QUIZ_ROUTES),
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatRadioModule,
    MatProgressSpinnerModule,
    MatIconModule
  ]
})
export class QuizzesModule { } 