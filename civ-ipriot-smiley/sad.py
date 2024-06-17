from smiley import Smiley
import time


class Sad(Smiley):  ## same thing
    def __init__(self):
        super().__init__(complexion=self.BLUE)
        self.draw_mouth()
        self.draw_eyes()

    def draw_mouth(self):
        """
        Method that draws the mouth on the standard faceless smiley.
        """
        mouth = [49, 54, 42, 43, 44, 45]
        for pixel in mouth:
            self.pixels[pixel] = self.BLANK

    def draw_eyes(self, wide_open=True):
        """
        Method that draws the eyes (open or closed) on the standard smiley.
        :param wide_open: True if eyes opened, False otherwise
        """
        eyes = [10, 13, 18, 21]
        for pixel in eyes:
            self.pixels[pixel] = self.BLANK if wide_open else self.complexion()
    
    def blink(self, delay=0.25):
        pass       
        blinking_eyes = [10, 13, 18, 21] ## locates the pixels
        count = 0    
        while count != 10:  ## loops this 10 times
            for pixel in blinking_eyes:
                self.pixels[pixel] = self.complexion()  ## turns the pixels to yellow
            self.show()
            time.sleep(delay)
            for pixel in blinking_eyes:
                self.pixels[pixel] = self.BLANK ## turns the pixels to black
            self.show()
            time.sleep(delay)
            count += 1                      ## adds 1 to count, till it is 10, then stops