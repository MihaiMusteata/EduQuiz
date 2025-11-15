from pydantic import BaseModel
from typing import Literal, List
from enum import Enum

class QuestionType(Enum):
    singleChoice = 0
    multipleChoice = 1
    trueFalse = 2
    shortAnswer = 3

class Answer(BaseModel):
    text: str
    isCorrect: bool

class QuizQuestion(BaseModel):
    text: str
    type: QuestionType
    hint: str
    answerOptions: List[Answer]

class QuizRequest(BaseModel):
    subject: str
    topic: str
    numQuestions: int
    language: str = "English"
