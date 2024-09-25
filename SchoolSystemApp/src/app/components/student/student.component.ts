import { Component, OnInit } from '@angular/core';
import { StudentService } from '../../services/student.service';
import { Student } from '../../models/Student';
import { Course } from '../../models/Course';
import { FormBuilder, FormsModule} from '@angular/forms';
import { CourseService } from '../../services/course.service';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-student',
  standalone: true,
  imports: [CommonModule, HttpClientModule, FormsModule],
  templateUrl: './student.component.html',
  styleUrl: './student.component.css'
})
export class StudentComponent implements OnInit {

  // Propiedades para enlazar con el formulario
  firstName: string = '';
  lastName: string = '';
  age: number | null = null;
  dateOfBirth: string = '';
  address: string = '';
  courseId: number | null = null;

  students: Student[] = [];
  courses: Course[] = [];
  editing: boolean = false;
  currentStudentId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private studentService: StudentService,
    private courseService: CourseService
  ) {
    
  }

  ngOnInit(): void {
    this.loadStudents();
    this.loadCourses();
  }

  // Cargar estudiantes
  loadStudents() {
    this.studentService.getStudents().subscribe(data => {
      this.students = data;
    });
  }

  // Cargar cursos
  loadCourses() {
    this.courseService.getCourses().subscribe(data => {
      this.courses = data;
    });
  }

  // Añadir o editar estudiante
  submitForm() {
    const student: Student = {
      Id: this.currentStudentId ?? 0, // ID solo si es una edición
      firstName: this.firstName,
      lastName: this.lastName,
      age: this.age!,
      dateOfBirth: new Date(this.dateOfBirth),
      address: this.address,
      courseId: this.courseId!,
      //course: this.courses.find(c => c.courseId === this.courseId) || { courseId: 0, name: '', description: '' }
    };

    if (this.editing && this.currentStudentId !== null) {
      this.studentService.updateStudent(this.currentStudentId, student).subscribe(() => {
        this.loadStudents();
        this.resetForm();
      });
    } else {
      this.studentService.addStudent(student).subscribe(() => {
        this.loadStudents();
        this.resetForm();
      });
    }
  }

  // Editar estudiante
  editStudent(student: Student) {
    this.editing = true;
    this.currentStudentId = student.Id;
    this.firstName = student.firstName;
    this.lastName = student.lastName;
    this.age = student.age;
    this.dateOfBirth = student.dateOfBirth.toISOString().split('T')[0]; // Convertir la fecha a un formato adecuado
    this.address = student.address;
    this.courseId = student.courseId;
  }

  // Borrar estudiante
  deleteStudent(studentId: number) {
    this.studentService.deleteStudent(studentId).subscribe(() => {
      this.loadStudents();
    });
  }

  // Buscar estudiante
  searchStudents(name: string) {
    this.studentService.searchStudents(name).subscribe(data => {
      this.students = data;
    });
  }

  // Reiniciar formulario
  resetForm() {
    this.editing = false;
    this.currentStudentId = null;
    this.firstName = '';
    this.lastName = '';
    this.age = null;
    this.dateOfBirth = '';
    this.address = '';
    this.courseId = null;
  }
}