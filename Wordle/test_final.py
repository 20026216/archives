# 20026216, coded by John Collin Bautista Maraan
import random

valid_words_file = './word-bank/all_words.txt'
target_words_file = './word-bank/target_words.txt'

MAX_TRIES = 6
tries = 0

filehandle = open(valid_words_file, 'r')
file_content = filehandle.read()
words = file_content.split()
filehandle.close()

targethandle = open(target_words_file, 'r')
target_content = targethandle.read()
words_target = target_content.split()
targethandle.close()

target_word = random.choice(words_target)

print("Welcome to Wordle! \nFor game rules, write 'help' \nTo exit the game, write 'exit' ")

while tries < MAX_TRIES:
    guess = input("Enter guess: ")
    guess = guess.lower()

    if guess == 'help':
        print("\nWelcome to Wordle")
        print("Wordle is a word guessing game, try to guess the right 5 letter word!")
        print("You will have 6 attempts to guess the correct word")
        print("There will be clues given to you when you've answered wrong")
        print("When the clue = ?: The letter you've put is not in the word")
        print("When the clue = |: The letter you've put is in the word, but it is not on the right spot")
        print("When the clue = X: The letter you've put is in the word, and the right spot!")
        print("Hope you answer the word correct!\n")
        continue
    if guess == 'exit':
        print(f"Thanks for trying! The correct word was {target_word}")
        break
    if guess not in words:
        print('Invalid guess, try 5 letter word')
        continue

    tries += 1

    if guess == target_word:
        print(f"Your guess is correct! You got wordle in: {tries}")
        name = input("Enter your name: ")
        if name == '':
            name = 'Anonymous'
        print(f"Thanks for putting your name: '{name}' your score is now logged")
        logged_scores = open("./score_log.txt", "a")
        logged_scores.write(f"{name} got wordle in: {tries}, the target word is: {target_word}\n")
        logged_scores.close()
        break

    else:
        clue = ["?", "?", "?", "?", "?"]
        target_word_list = list(target_word)
        guess_list = list(guess)

        for letter in range(5):
            if guess_list[letter] == target_word_list[letter]:
                guess_list[letter] = '&'
                target_word_list[letter] = '='
                clue[letter] = 'X'

        for letter in range(5):
            if guess_list[letter] in target_word_list:
                clue[letter] = '|'
                target_word_list.remove(guess_list[letter])

        # code sampled from Ingrid Aldum
        clues = " ".join(clue)
        guesses = " ".join(guess)
        print(f"Your guess is wrong! You have: {MAX_TRIES - tries} tries")
        print(f"Guess: {guesses}")
        print(f"Clue:  {clues}")

    if tries == MAX_TRIES:
        print(f"Sorry, you've reached the maximum number of tries.")
        name_loss = input("Enter your name: ")
        if name_loss == '':
            name_loss = 'Anonymous'
        print(f"Sorry '{name_loss}' The correct word was {target_word}, your score is now logged.")
        logged_fail = open("./score_log.txt", "a")
        logged_fail.write(f"{name_loss} failed attempt at: {tries} tries, the target word is: {target_word}\n")
        logged_fail.close()
