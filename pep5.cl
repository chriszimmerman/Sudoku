(defun p5 (number)
  (checkDiv number number))

(defun checkDiv (originalNumber currentNumber)
  (cond ((eq (checkDiv2 currentNumber originalNumber) t) currentNumber)
	(t (checkDiv originalNumber (+ currentNumber originalNumber)))))

(defun checkDiv2 (currentNumber currentDiv)
  (cond ((<= currentDiv 1) t)
	((eq (mod currentNumber currentDiv) 0) 
	 (checkDiv2 currentNumber (- currentDiv 1)))
	(t nil)))
	
	
