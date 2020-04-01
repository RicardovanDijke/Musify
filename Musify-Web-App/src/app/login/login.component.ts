import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  public errorMsg: string | undefined = undefined;

  constructor(    private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthService

    ) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  // convenience getter for easy access to form fields
  get f() { return this.loginForm.controls; }


  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;
    this.authService.login(this.f.username.value, this.f.password.value)
      .subscribe(
        data => {
          console.log(data)

          if (data != null) {
            this.errorMsg = undefined;
            //this.authService.setBearerToken(data.token)

            window.sessionStorage.setItem('loggedUsername', data.username);

            this.router.navigate([this.returnUrl]);
          } else {

            this.loading = false;
            console.log(data)
            this.errorMsg = "Invalid Username or Password combination.";
          }
        },
        
        error => {
          console.log("error logging in: ", error)

          this.errorMsg = "Error connecting to backend."
          this.loading = false;
        });
  }

  hasErrorMsg(){

  }
}
