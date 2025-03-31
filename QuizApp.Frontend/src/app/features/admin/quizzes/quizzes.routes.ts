import { Routes } from '@angular/router';

export const QUIZZES_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () => import('./quizzes-list/quizzes-list.component').then(m => m.QuizzesListComponent)
  },
  {
    path: 'new',
    loadComponent: () => import('./quiz-form/quiz-form.component').then(m => m.QuizFormComponent)
  },
  {
    path: ':id',
    loadComponent: () => import('./quiz-form/quiz-form.component').then(m => m.QuizFormComponent)
  }
]; 