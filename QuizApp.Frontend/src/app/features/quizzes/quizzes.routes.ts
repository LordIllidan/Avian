import { Routes } from '@angular/router';

export const QUIZ_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () => import('./components/quiz-list/quiz-list.component').then(m => m.QuizListComponent)
  },
  {
    path: 'new',
    loadComponent: () => import('./components/quiz-form/quiz-form.component').then(m => m.QuizFormComponent)
  },
  {
    path: ':id/attempt/:attemptId',
    loadComponent: () => import('./components/quiz-attempt/quiz-attempt.component').then(m => m.QuizAttemptComponent)
  }
]; 