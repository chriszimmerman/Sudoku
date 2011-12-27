(defun fp (number)
  (loop for i from 2 to (sqrt number)
	do (if (and (isFactor i number) (primeP i)) (print i))))

(defun isFactor (factorCandidate number)
  (if (eq (mod number factorCandidate) 0) t nil))

(defun primeP (number)
  (if (eq (length (getFactorsBase number)) 1) t nil))

(defun primeBase (number)
	(primeEx 2 number))

(defun primeEx (factorCandidate number)
	(cond ((>= factorCandidate (sqrt number)) t) 
	      ((eq (mod number factorCandidate) 0) nil)
	      (t (primeEx (+ factorCandidate 1) number))))

(defun getFactorsBase (number)
  (getFactors 2 number))

(defun getFactors (factorCandidate number)
  (cond ((isFactor factorCandidate number) 
	 (cons factorCandidate (getFactors (+ 2 factorCandidate) number)))
	((> factorCandidate (sqrt number)) (cons number nil))
	(t (getFactors (+ 2 factorCandidate) number))))
