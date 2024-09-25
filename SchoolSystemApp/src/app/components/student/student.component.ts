import { Component, OnInit } from '@angular/core';
import { StudentService } from '../../services/student.service';
import { Student } from '../../models/Student';
import { Course } from '../../models/Course';
import { FormBuilder, FormsModule} from '@angular/forms';
import { CourseService } from '../../services/course.service';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import Swal from 'sweetalert2';

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
  searchTerm: string = '';

  students: Student[] = [];
  courses: Course[] = [];
  editing: boolean = false;
  currentStudentId: number | null = null;

  // Propiedades para el formulario de cursos
  courseName: string = '';
  editingCourse: boolean = false;
  currentCourseId: number | null = null;
  courseDescription: string = '';

  constructor(
    private fb: FormBuilder,
    private studentService: StudentService,
    private courseService: CourseService
  ) {}
    
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
      id: this.currentStudentId ?? 0, // ID solo si es una edición
      firstName: this.firstName,
      lastName: this.lastName,
      age: this.age!,
      dateOfBirth: new Date(this.dateOfBirth),
      address: this.address,
      courseId: this.courseId!,
      courseName:''
    };

    if (this.editing && this.currentStudentId !== null) {
      this.studentService.updateStudent(this.currentStudentId, student).subscribe(() => {
        this.loadStudents();
        this.resetForm();
        Swal.fire('Updated!', 'Student details have been updated successfully.', 'success'); // SweetAlert para éxito
      });
    } else {
      this.studentService.addStudent(student).subscribe(() => {
        this.loadStudents();
        this.resetForm();
        Swal.fire('Added!', 'Student has been added successfully.', 'success'); // SweetAlert para éxito
      });
    }
  }

    // Añadir o editar curso
    submitCourse() {
      const course: Course = {
        id: this.currentCourseId ?? 0,
        name: this.courseName,
        description: this.courseDescription
      };
  
      if (this.editingCourse && this.currentCourseId !== null) {
        this.courseService.updateCourse(this.currentCourseId, course).subscribe(() => {
          this.loadCourses();
          this.resetCourseForm();
          Swal.fire('Updated!', 'Course has been updated successfully.', 'success');
        });
      } else {
        this.courseService.createCourse(course).subscribe(() => {
          this.loadCourses();
          this.resetCourseForm();
          Swal.fire('Added!', 'Course has been added successfully.', 'success');
        });
      }
    }

     // Editar curso
  editCourse(course: Course) {
    this.editingCourse = true;
    this.currentCourseId = course.id;
    this.courseName = course.name;
    this.courseDescription = course.description;

    // Abrir modal si es necesario (aunque ya está abierto por el botón)
  }

  // Eliminar curso
  deleteCourse(courseId: number) {
    Swal.fire({
      title: 'Are you sure?',
      text: 'Do you really want to delete this course?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, cancel!'
    }).then((result) => {
      if (result.isConfirmed) {
        this.courseService.deleteCourse(courseId).subscribe(() => {
          this.loadCourses();
          Swal.fire('Deleted!', 'Course has been deleted successfully.', 'success');
        });
      }
    });
  }

  // Editar estudiante
  editStudent(student: Student) {
    this.editing = true;
    this.currentStudentId = student.id;
    this.firstName = student.firstName;
    this.lastName = student.lastName;
    this.age = student.age;
    this.dateOfBirth = student.dateOfBirth.toISOString().split('T')[0]; // Convertir la fecha a un formato adecuado
    this.address = student.address;
    this.courseId = student.courseId;
  }

  // Borrar estudiante
  deleteStudent(studentId: number) {
    Swal.fire({
      title: 'Are you sure?',
      text: 'Do you really want to delete this student?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, cancel!'
    }).then((result) => {
      if (result.isConfirmed) {
        this.studentService.deleteStudent(studentId).subscribe(() => {
          this.loadStudents();
          Swal.fire('Deleted!', 'Student has been deleted successfully.', 'success'); // SweetAlert para éxito
        });
      }
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

   // Reiniciar el formulario de curso
   resetCourseForm() {
    this.editingCourse = false;
    this.currentCourseId = null;
    this.courseName = '';
  }

  getFilteredStudents(): Student[] {
    if (!this.searchTerm) {
      return this.students;
    }
    
    return this.students.filter(student =>
      student.firstName.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
      student.lastName.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }
  
}