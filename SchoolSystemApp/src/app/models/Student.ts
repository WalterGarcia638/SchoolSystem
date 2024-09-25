import { Course } from "./Course";

export interface Student {
    id: number;
    firstName: string;
    lastName: string;
    age: number;
    dateOfBirth: Date;
    address: string;
    courseId: number;
    courseName?: string;
    course?: Course;
  }