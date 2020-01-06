import { Injectable } from '@angular/core';

@Injectable({
   providedIn: 'root'
})

export class Patterns {
   email = /^[a-zA-Z]{1}[a-zA-Z0-9.\-_]*@[a-zA-Z]{1}[a-zA-Z.-]*[a-zA-Z]{1}[.][a-zA-Z]{2,4}$/;

}