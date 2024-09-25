import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Student } from '../models/Student';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  private apiUrlStudents = 'https://localhost:7127/api/Student';

  constructor(private http: HttpClient) { }

   // Obtener todos los estudiantes
   getStudents(): Observable<Student[]> {
    return this.http.get<Student[]>(this.apiUrlStudents);
  }

  // Añadir un nuevo estudiante
  addStudent(student: Student): Observable<Student> {
    return this.http.post<Student>(this.apiUrlStudents, student);
  }

  // Editar un estudiante existente
  updateStudent(id: number, student: Student): Observable<void> {
    return this.http.put<void>(`${this.apiUrlStudents}/${id}`, student);
  }

  // Borrar un estudiante
  deleteStudent(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrlStudents}/${id}`);
  }

  // Buscar estudiantes por nombre o apellido
  searchStudents(name: string): Observable<Student[]> {
    return this.http.get<Student[]>(`${this.apiUrlStudents}/search/${name}`);
  }

}
