import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, Router } from '@angular/router';
import { AuthHelper } from '../helpers/auth.helper';
import { UserRole } from '../enums/user-roles';

@Injectable({providedIn: 'root'})

export class RoleGuard implements CanActivate {

    constructor(
        private authHelper: AuthHelper,
        private router: Router
    ) {
    }

    async canActivate(route: ActivatedRouteSnapshot): Promise<boolean> {
        let role = await this.authHelper.getRoleFromToken();

        let currentRoles: number[] = route.data.roles;

        if (!currentRoles) {
            return false;
        }

        if (!role) {
            this.router.navigate(['auth']);
            return false;
        }

        let isInRole = currentRoles.some(x => x === UserRole[role]);

        if (isInRole) {
            return true;
        }

        this.router.navigate(['management']);
        return false;
    }
    
}