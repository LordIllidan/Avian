import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { firstValueFrom } from 'rxjs';

export const roleGuard = async (route: any) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const requiredRoles = route.data['roles'] as string[];

  if (!requiredRoles || requiredRoles.length === 0) {
    return true;
  }

  const user = await firstValueFrom(authService.currentUser$);
  if (user?.role && requiredRoles.includes(user.role)) {
    return true;
  }

  return router.createUrlTree(['/quizzes']);
}; 