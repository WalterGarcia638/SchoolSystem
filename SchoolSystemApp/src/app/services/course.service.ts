import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Course } from '../models/Course';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  private apiUrlCourses  = 'https://localhost:7127/api/Course';

  constructor(private http: HttpClient) { }

    // Obtener todos los cursos
    getCourses(): Observable<Course[]> {
      return this.http.get<Course[]>(this.apiUrlCourses);
    }

}
