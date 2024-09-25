import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  username = '';
  password = '';

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
    this.authService.login(this.username, this.password).subscribe(
      () => {
        // Alerta de éxito
        Swal.fire({
          icon: 'success',
          title: 'Login Successful',
          text: 'Welcome!',
          showConfirmButton: false,
          timer: 1000
        });

        // Navegar a la página de estudiantes
        this.router.navigate(['/students']);
      },
      (error) => {
        // Alerta de error
        Swal.fire({
          icon: 'error',
          title: 'Login Failed',
          text: 'Invalid username or password',
        });

        console.error('Login failed', error);
      }
    );
  }

}