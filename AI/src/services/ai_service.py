from langchain_groq import ChatGroq
from langchain_core.prompts import ChatPromptTemplate
import json
from typing import List, Dict

from src.config.settings import GROQ_API_KEY, MODEL_NAME
from src.services.prompt_templates import QUIZ_PROMPT_TEMPLATE

def _is_valid_quiz_item(item: dict) -> bool:
    return (
        isinstance(item, dict) and
        "text" in item and
        "type" in item and
        "answers" in item and
        isinstance(item["answers"], list) and
        all(isinstance(ans, dict) and "text" in ans and "isCorrect" in ans for ans in item["answers"])
    )

llm = ChatGroq(
    api_key=GROQ_API_KEY,
    model=MODEL_NAME,
    temperature=0.7,
    max_tokens=4000,
)

quiz_prompt = ChatPromptTemplate.from_messages(QUIZ_PROMPT_TEMPLATE)

def _get_llm_json(prompt: str, validation_type: str) -> List[Dict]:
    try:
        response = llm.invoke(prompt)
        buffer = response.content
        items = json.loads(buffer)
    except Exception as e:
        return [{"error": str(e)}]

def get_quiz(subject: str, topic: str, num_questions: int, language: str) -> str:
    formatted_prompt = quiz_prompt.format(
        subject=subject,
        topic=topic,
        num_questions=num_questions,
        language=language
    )
    try:
        response = llm.invoke(formatted_prompt)
        buffer = response.content
        return json.loads(buffer)
        items = json.loads(buffer)
    except Exception as e:
        return [{"error": str(e)}]
