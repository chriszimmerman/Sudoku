(defun p5 (number)
  (checkDiv number number))

(defun checkDiv (originalNumber currentNumber)
  (loop for i from (* originalNumber (- originalNumber 1))
	by (* originalNumber (- originalNumber 1)) 
	until (eq t (checkDiv2 i originalNumber)) 
	do (print (+ (* originalNumber (- originalNumber 1)) i))))

(defun checkDiv2 (currentNumber currentDiv)
  (cond ((<= currentDiv 1) t)
	((eq (mod currentNumber currentDiv) 0) 
	 (checkDiv2 currentNumber (- currentDiv 1)))
	(t nil)))
	
	
