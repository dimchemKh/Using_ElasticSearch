import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, Router } from '@angular/router';
import { AuthHelper } from '../helpers/auth.helper';
import { UserRoles } from '../enums/user-roles';

@Injectable({providedIn: 'root'})

export class RoleGuard implements CanActivate {

    constructor(
        private authHelper: AuthHelper,
        private router: Router
    ) {

    }

    canActivate(route: ActivatedRouteSnapshot): boolean {
        debugger;
        let role = this.authHelper.getRoleFromToken();

        let currentRoles: number[] = route.data.roles;

        if (!currentRoles) {
            return false;
        }
        
        let isInRole = currentRoles.some(x => x == UserRoles[role]);

        if (isInRole) {
            return true;
        }

        this.router.navigate(['auth']);
        return false;
    }
    
}