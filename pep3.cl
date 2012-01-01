(defun fp (number)
  (loop for i from 2 to (sqrt number)
	do (if (isFactor i number) (printIfItsAPrimeFactor i))
	do (if (isFactor i number) (printIfItsAPrimeFactor (/ number i)))))

(defun isFactor (factorCandidate number)
  (if (eq (mod number factorCandidate) 0) t nil))

(defun printIfItsAPrimeFactor (number)
  (if (primeBase number) (print number)))

(defun primeBase (number)
	(primeEx 2 number))

(defun primeEx (factorCandidate number)
	(cond ((> factorCandidate (sqrt number)) t) 
	      ((eq (mod number factorCandidate) 0) nil)
	      (t (primeEx (+ factorCandidate 1) number))))