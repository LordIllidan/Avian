import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { QuizService, Quiz, QuizAttempt, QuestionResponse } from '../../../../services/quiz.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-quiz-attempt',
  templateUrl: './quiz-attempt.component.html',
  styleUrls: ['./quiz-attempt.component.scss']
})
export class QuizAttemptComponent implements OnInit {
  quiz: Quiz | null = null;
  attempt: QuizAttempt | null = null;
  quizForm: FormGroup;
  loading = true;
  error: string | null = null;
  submitting = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private quizService: QuizService,
    private fb: FormBuilder
  ) {
    this.quizForm = this.fb.group({});
  }

  ngOnInit(): void {
    const quizId = Number(this.route.snapshot.paramMap.get('id'));
    const attemptId = Number(this.route.snapshot.paramMap.get('attemptId'));

    this.quizService.getQuiz(quizId).subscribe({
      next: (quiz) => {
        this.quiz = quiz;
        this.attempt = {
          id: attemptId,
          quizId,
          userId: 0, // Will be set by the backend
          startedAt: new Date(),
          score: 0,
          responses: []
        };

        // Create form controls for each question
        quiz.questions.forEach(question => {
          this.quizForm.addControl(
            `question_${question.id}`,
            this.fb.control('', Validators.required)
          );
        });

        this.loading = false;
      },
      error: (error) => {
        this.error = 'Failed to load quiz';
        this.loading = false;
      }
    });
  }

  onSubmit(): void {
    if (this.quizForm.valid && this.quiz && this.attempt) {
      this.submitting = true;

      const responses: Omit<QuestionResponse, 'id' | 'quizAttemptId' | 'isCorrect'>[] = 
        this.quiz.questions.map(question => ({
          questionId: question.id,
          selectedAnswerId: Number(this.quizForm.get(`question_${question.id}`)?.value)
        }));

      this.quizService.submitQuizAttempt(
        this.quiz.id,
        this.attempt.id,
        responses
      ).subscribe({
        next: (attempt) => {
          this.router.navigate(['/quizzes']);
        },
        error: (error) => {
          this.error = 'Failed to submit quiz';
          this.submitting = false;
        }
      });
    }
  }
} 