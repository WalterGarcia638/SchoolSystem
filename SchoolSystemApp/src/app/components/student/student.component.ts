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

  loadStudents() {
    this.studentService.getStudents().subscribe(data => {
      this.students = data;
    });
  }

  loadCourses() {
    this.courseService.getCourses().subscribe(data => {
      this.courses = data;
    });
  }

  submitForm() {
    const student: Student = {
      id: this.currentStudentId ?? 0, 
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
        Swal.fire('Updated!', 'Estudiante actualizado correctamente.', 'success'); 
      });
    } else {
      this.studentService.addStudent(student).subscribe(() => {
        this.loadStudents();
        this.resetForm();
        Swal.fire('Added!', 'Estudiante agregado correctamente.', 'success'); 
      });
    }
  }

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
          Swal.fire('Updated!', 'Curso actualizado correctamente.', 'success');
        });
      } else {
        this.courseService.createCourse(course).subscribe(() => {
          this.loadCourses();
          this.resetCourseForm();
          Swal.fire('Added!', 'Curso añadido correctamente.', 'success');
        });
      }
    }

  editCourse(course: Course) {
    this.editingCourse = true;
    this.currentCourseId = course.id;
    this.courseName = course.name;
    this.courseDescription = course.description;

  }

  deleteCourse(courseId: number) {
    Swal.fire({
      title: 'Estas seguro?',
      text: '¿Realmente desea eliminar este curso?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'si',
      cancelButtonText: 'No, Cancelar!'
    }).then((result) => {
      if (result.isConfirmed) {
        this.courseService.deleteCourse(courseId).subscribe(() => {
          this.loadCourses();
          Swal.fire('Deleted!', 'Curso Eliminado satisfactoriamente.', 'success');
        });
      }
    });
  }

 
  editStudent(student: Student) {

    console.log(student)
    this.editing = true;
    this.currentStudentId = student.id;
    this.firstName = student.firstName;
    this.lastName = student.lastName;
    this.age = student.age;
    this.dateOfBirth = student.dateOfBirth.toISOString().split('T')[0]; 
    this.address = student.address;
    this.courseId = student.courseId;
  }

  
  deleteStudent(studentId: number) {
    Swal.fire({
      title: 'Estas seguro?',
      text: '¿Realmente desea eliminar este estudiante?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Si',
      cancelButtonText: 'No, Cancelar!'
    }).then((result) => {
      if (result.isConfirmed) {
        this.studentService.deleteStudent(studentId).subscribe(() => {
          this.loadStudents();
          Swal.fire('Deleted!', 'Estudiante eliminado satisfactoriamente.', 'success'); 
        });
      }
    });
  }

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