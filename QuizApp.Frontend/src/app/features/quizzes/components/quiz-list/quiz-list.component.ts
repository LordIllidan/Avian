import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { QuizService, Quiz } from '../../../../services/quiz.service';

@Component({
  selector: 'app-quiz-list',
  templateUrl: './quiz-list.component.html',
  styleUrls: ['./quiz-list.component.scss']
})
export class QuizListComponent implements OnInit {
  quizzes: Quiz[] = [];
  loading = true;
  error: string | null = null;

  constructor(
    private quizService: QuizService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadQuizzes();
  }

  loadQuizzes(): void {
    this.quizService.getQuizzes().subscribe({
      next: (quizzes) => {
        this.quizzes = quizzes;
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Failed to load quizzes';
        this.loading = false;
      }
    });
  }

  startQuiz(quizId: number): void {
    this.quizService.startQuiz(quizId).subscribe({
      next: (attempt) => {
        this.router.navigate(['/quizzes', quizId, 'attempt', attempt.id]);
      },
      error: (error) => {
        this.error = 'Failed to start quiz';
      }
    });
  }
} 