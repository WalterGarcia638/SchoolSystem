import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { StudentComponent } from "./components/student/student.component";
import { LoginComponent } from "./components/login/login.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, StudentComponent, LoginComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'SchoolSystemApp';
}
