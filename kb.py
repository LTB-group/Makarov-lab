from idlelib.editor import keynames

from aiofiles.os import replace
from aiogram import Bot, Dispatcher, types, F
from aiogram.types import ReplyKeyboardMarkup, KeyboardButton, InlineKeyboardMarkup, InlineKeyboardButton
from aiogram.filters import Command
from selenium.webdriver.common.utils import keys_to_typing
from unicodedata import category

categories = {
    "Жаропонижающие": ["Парацетомол", "Ибупрофен"],
    "Антибиотики": ["Амоксициллин", "Ципрофлоксацин"],
    "Витамины": ["Витамин С", "Витамин D"]
}

# Информация о лекарствах
medicines_info = {
    "Парацетомол": {"description": "Обезболивающее и жаропонижающее средство.", "price": 100},
    "Ибупрофен": {"description": "Противовоспалительное и обезболивающее средство.", "price": 150},
    "Амоксициллин": {"description": "Антибиотик широкого спектра действия.", "price": 200},
    "Ципрофлоксацин": {"description": "Антибиотик для лечения инфекций.", "price": 250},
    "Витамин С": {"description": "Витамин для поддержания иммунитета.", "price": 50},
    "Витамин D": {"description": "Витамин для здоровья костей.", "price": 75}
}


def get_admin_panel():
    buttons = [
        [KeyboardButton(text="Информация о заказе")],
        [
            KeyboardButton(text="Информация о заказе"),
            KeyboardButton(text="Информация о заказе"),
        ],
    ]
    keyboard = ReplyKeyboardMarkup(keyboard=buttons, resize_keyboard=True)
    return keyboard

def get_main_menu():
    buttons = [
        [KeyboardButton(text="Каталог")],
        [KeyboardButton(text="Поиск")],
        [KeyboardButton(text="Корзина")],
        [KeyboardButton(text="Связь с оператором")],
    ]
    keyboard = ReplyKeyboardMarkup(keyboard=buttons, resize_keyboard=True)
    return keyboard

def get_catalog():
    keyboard = InlineKeyboardMarkup(inline_keyboard=[
        [InlineKeyboardButton(text=category, callback_data=f"category_{category}")]
        for category in categories
    ])
    return keyboard

def get_medicines_in_category(callback_query: types.CallbackQuery):
    category = callback_query.data.split('_')[1]
    keyboard = InlineKeyboardMarkup(inline_keyboard=[
        [InlineKeyboardButton(text=medicine, callback_data=f"medicine_{medicine}")]
        for medicine in categories[category]
    ])
    return keyboard

def get_show_medicine_info(callback_query: types.CallbackQuery):
    medicine = callback_query.data.split('_')[1]
    keyboard = InlineKeyboardMarkup(inline_keyboard=[
        [InlineKeyboardButton(text="Добавить в корзину", callback_data=f"addtocart_{medicine}")],
    ])
    return keyboard