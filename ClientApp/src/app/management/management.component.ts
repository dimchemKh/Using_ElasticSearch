import { Component, OnInit } from '@angular/core';
import { AuthHelper } from '../shared/helpers/auth.helper';
import { UserRoles } from '../shared/enums/user-roles';
import { Router } from '@angular/router';

@Component({
  selector: 'app-managment',
  templateUrl: './management.component.html',
  styleUrls: ['./management.component.scss']
})
export class ManagementComponent implements OnInit {

  isAdmin: boolean;

  constructor(
    private authHelper: AuthHelper,
    private router: Router
  ) { 
    this.isAdmin = true;
  }

  logOut(): void {
    this.authHelper.logOut();
    this.router.navigate(['auth']);
  }

  async ngOnInit() {
    let role = await this.authHelper.getRoleFromToken().then(x => x);
    
    if (role === UserRoles[UserRoles.Admin]) {
      this.isAdmin = false;
    }
  }

}
