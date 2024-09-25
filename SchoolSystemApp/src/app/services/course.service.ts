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
 // Crear un curso
 createCourse(course: Course): Observable<any> {
  return this.http.post(this.apiUrlCourses, course);
}

// Actualizar un curso
updateCourse(id: number, course: Course): Observable<any> {
  return this.http.patch(`${this.apiUrlCourses}/${id}`, course);
}

// Eliminar un curso
deleteCourse(id: number): Observable<any> {
  return this.http.delete(`${this.apiUrlCourses}/${id}`);
}
}