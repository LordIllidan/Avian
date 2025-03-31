import { Routes } from '@angular/router';
import { AdminComponent } from './admin.component';

export const ADMIN_ROUTES: Routes = [
  {
    path: '',
    component: AdminComponent,
    children: [
      {
        path: 'quizzes',
        loadChildren: () => import('./quizzes/quizzes.routes').then(m => m.QUIZZES_ROUTES)
      },
      {
        path: 'users',
        loadChildren: () => import('./users/users.routes').then(m => m.USERS_ROUTES)
      },
      {
        path: '',
        redirectTo: 'quizzes'
      }
    ]
  }
]; 