import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { AuthService } from './auth.service';

export interface Quiz {
  id: number;
  title: string;
  description: string;
  timeLimit: number;
  questions: Question[];
}

export interface Question {
  id: number;
  text: string;
  answers: Answer[];
}

export interface Answer {
  id: number;
  text: string;
  isCorrect: boolean;
}

export interface QuizAttempt {
  id: number;
  quizId: number;
  userId: number;
  startedAt: Date;
  score: number;
  responses: QuestionResponse[];
}

export interface QuestionResponse {
  id: number;
  quizAttemptId: number;
  questionId: number;
  selectedAnswerId: number;
  isCorrect: boolean;
}

export interface QuizResults {
  totalAttempts: number;
  averageScore: number;
  attempts: {
    userId: number;
    score: number;
    completedAt: Date;
  }[];
}

@Injectable({
  providedIn: 'root'
})
export class QuizService {
  private apiUrl = `${environment.apiUrl}/api/quizzes`;

  constructor(
    private http: HttpClient,
  ) {}

  getQuizzes(): Observable<Quiz[]> {
    return this.http.get<Quiz[]>(this.apiUrl);
  }

  getQuiz(id: number): Observable<Quiz> {
    return this.http.get<Quiz>(`${this.apiUrl}/${id}`);
  }

  createQuiz(quiz: Omit<Quiz, 'id' | 'createdAt'>): Observable<Quiz> {
    return this.http.post<Quiz>(`${environment.apiUrl}/api/quizzes`, quiz);
  }

  startQuiz(quizId: number): Observable<QuizAttempt> {
    return this.http.post<QuizAttempt>(`${this.apiUrl}/${quizId}/attempts`, {});
  }

  submitQuizAttempt(quizId: number, attemptId: number, responses: Omit<QuestionResponse, 'id' | 'quizAttemptId' | 'isCorrect'>[]): Observable<QuizAttempt> {
    return this.http.post<QuizAttempt>(`${this.apiUrl}/${quizId}/attempts/${attemptId}/submit`, { responses });
  }

  getQuizResults(quizId: number): Observable<QuizResults> {
    return this.http.get<QuizResults>(`${environment.apiUrl}/api/quiz/${quizId}/results`);
  }
} 