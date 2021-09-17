import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginModel } from './login.model';
import { LoginService } from './login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  providers: [LoginService]
})
export class LoginComponent implements OnInit {
  form: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  validacionaPoruka: string = "";

  constructor(
      private formBuilder: FormBuilder,
      private route: ActivatedRoute,
      private router: Router,
      private loginService: LoginService
  ) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
        username: ['', Validators.required],
        password: ['', Validators.required]
    });

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  // convenience getter for easy access to form fields
  get f() { return this.form.controls; }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.form.invalid) {
        return;
    }

    this.loading = true;
    var loginModel: LoginModel =  {
      username: this.f.username.value,
      password: this.f.password.value
    };
    this.loginService.login(loginModel).subscribe(response => {
      if(response.success){
        this.validacionaPoruka = "";
        //this.router.navigate([`/home`]);
        this.loading = false;
        this.router.navigate([this.returnUrl]);
      }else{
        this.validacionaPoruka = response.message;
        this.loading = false;
      }
    });
  }
}