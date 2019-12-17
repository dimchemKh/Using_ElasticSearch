import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

const EMAIL_PATTERN = /^[a-zA-Z]{1}[a-zA-Z0-9.\-_]*@[a-zA-Z]{1}[a-zA-Z.-]*[a-zA-Z]{1}[.][a-zA-Z]{2,4}$/;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public hide: boolean;
  public email: FormControl;
  public password: FormControl;

  constructor(
  ) {
    this.hide = true;
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

  private checkError(controll: string, patternError: string, requiredError: string): string {
    if (this[controll].hasError('pattern')) {
      return patternError;
    }
    if (this[controll].hasError('required')) {
      return requiredError;
    }
  }

  getEmailErrorMessage(): string {
    return this.checkError('email', 'Incorrect email', 'Empty email');
  }

  getPasswordErrorMessage(): string {
    return this.checkError('password', '', 'Empty password');
  }

  ngOnInit() {
  }

}
