import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { AuthentificationService } from 'src/app/core/services/authentification.service';
import { RequestLoginAuthentificationView } from 'src/app/shared/models/authentification/request/request-login-authentification-view';
import { AuthHelper } from 'src/app/shared/helpers/auth.helper';

const EMAIL_PATTERN = /^[a-zA-Z]{1}[a-zA-Z0-9.\-_]*@[a-zA-Z]{1}[a-zA-Z.-]*[a-zA-Z]{1}[.][a-zA-Z]{2,4}$/;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  hide: boolean;
  email: FormControl;
  password: FormControl;
  requestLogin: RequestLoginAuthentificationView;

  constructor(
    private authentificationService: AuthentificationService,
    private authHelper: AuthHelper
  ) {
    this.hide = true;
    this.requestLogin = new RequestLoginAuthentificationView();
    this.InitialControls();
  }

  private InitialControls(): void {
    this.email = new FormControl(null,
      [
        Validators.required,
        Validators.pattern(EMAIL_PATTERN)
      ]);
    this.password = new FormControl(null, Validators.required);
  }

  private checkError(control: string, patternError: string, requiredError: string): string {
    if (this[control].hasError('pattern')) {
      return patternError;
    }
    if (this[control].hasError('required')) {
      return requiredError;
    }
  }

  getEmailErrorMessage(): string {
    return this.checkError('email', 'Incorrect email', 'Empty email');
  }

  getPasswordErrorMessage(): string {
    return this.checkError('password', '', 'Empty password');
  }

  login(): void {
    if (this.email.valid && this.password.valid) {
      this.authentificationService.login(this.requestLogin).subscribe((data) => {
        this.authHelper.login(data);
      });
    }
  }
}
