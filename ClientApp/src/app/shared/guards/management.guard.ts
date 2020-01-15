import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, Router } from '@angular/router';
import { AuthHelper } from '../helpers/auth.helper';
import { UserRole } from '../enums/user-roles';

@Injectable({providedIn: 'root'})

export class ManagementGuard implements CanActivate {

    constructor(
        private authHelper: AuthHelper,
        private router: Router
    ) {
    }

    async canActivate(route: ActivatedRouteSnapshot): Promise<boolean> {
        let permission = await this.authHelper.getPermissionsFromToken();

        let currentRoles: number[] = route.data.roles;

        if (!currentRoles) {
            // return false;
        }

        if (!permission) {
            this.router.navigate(['auth']);
            return false;
        }

        let url = route.url.map(x => x.path)[0];

        let sp = url.split('-').map(x => x.substring(0, 1).toUpperCase() + x.substring(1)).join('');

        if (permission.includes(sp + '.' + 'CanView=true')) {
            return true;
        }
        
        // let isInRole = currentRoles.some(x => x === UserRole[permission]);

        // if (isInRole) {
        //     return true;
        // }

        this.router.navigate(['management']);
        return false;
    }
    
}