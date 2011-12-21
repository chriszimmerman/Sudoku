;Chris Zimmerman
;Project Euler problem 2
;Objective: Find the sum of even fibonacci terms under 4 million.

;base fibonacci function. recursively calls the fibonacci function.
;the parameter 'number' specifies the max fibonacci number to find
(defun fibBase (number)
  (cond ((eq number 1) 0)
	((eq number 2) 2)
	(t (append '(1 2) (fibonacci 1 2 number)))))

;recursive fibonacci function. if the next fib number is under the limit,
;call fibonacci again. returns a list of fib numbers to fibBase
(defun fibonacci (x y number)
  (if (<= (+ x y) number) (cons (+ x y) (fibonacci y (+ x y) number)) '() ))

;applies a filter to the fib number list to return even fib numbers
(defun getEvens (fibnumbers)
  (mapcan #'(lambda (number) (when (evenp number) (list number))) fibnumbers))

;returns a sum of the list of even fibonacci numbers
(defun getSumOfEvenFibs (number)
  (reduce #'+ (getEvens (fibBase number))))