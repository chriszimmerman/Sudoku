(defun p4 (numberOfDigits)
	(getProductOfTwoNumbers (first (starterNumbers numberOfDigits))
				 (second (starterNumbers numberOfDigits))))

(defun starterNumbers (number)
	(cons (- (expt 10 number) 1) (cons (- (expt 10 number) 1) '())))

(defun getProductOfTwoNumbers (number1 number2)
  (setf *largest* 0)
  (loop for i downfrom number1 to 1
	do (loop for j downfrom number2 to 1
		 do (if (and (palindromeP (prin1-to-string (* i j))) 
			     (> (* i j) *largest*)) (setq *largest* (* i j)))))
  (print *largest*))

(defun palindromeP (string)
  (if (equal (reverse string) string) t nil))