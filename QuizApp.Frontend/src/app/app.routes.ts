import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { authGuard } from './guards/auth.guard';
import { roleGuard } from './guards/role.guard';

export const routes: Routes = [
  { 
    path: 'login', 
    component: LoginComponent 
  },
  {
    path: 'quizzes',
    loadChildren: () => import('./features/quizzes/quizzes.routes').then(m => m.QUIZ_ROUTES),
    canActivate: [authGuard]
  },
  {
    path: 'admin',
    loadChildren: () => import('./features/admin/admin.routes').then(m => m.ADMIN_ROUTES),
    canActivate: [authGuard, roleGuard],
    data: { roles: ['Admin'] }
  },
  { 
    path: '', 
    redirectTo: '/quizzes', 
    pathMatch: 'full' 
  },
  { 
    path: '**', 
    redirectTo: '/quizzes', 
    pathMatch: 'full' 
  }
];
