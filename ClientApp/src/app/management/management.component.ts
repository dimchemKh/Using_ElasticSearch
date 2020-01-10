import { Component, OnInit } from '@angular/core';
import { AuthHelper } from '../shared/helpers/auth.helper';
import { UserRole } from '../shared/enums/user-roles';
import { Router } from '@angular/router';

@Component({
  selector: 'app-managment',
  templateUrl: './management.component.html',
  styleUrls: ['./management.component.scss']
})
export class ManagementComponent implements OnInit {

  nonAdmin: boolean;

  constructor(
    private authHelper: AuthHelper,
    private router: Router
  ) { 
    this.nonAdmin = true;
  }

  logOut(): void {
    this.authHelper.logOut();
    this.router.navigate(['auth']);
  }

  async ngOnInit() {
    let role = await this.authHelper.getRoleFromToken().then(x => x);
    
    if (role === UserRole[UserRole.SysAdmin]) {
      this.nonAdmin = false;
    }
  }

}
