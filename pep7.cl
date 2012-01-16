(defun p7 (number)
  (findThisManyPrimes number))

(defun findThisManyPrimes (number)
  (setf *primes* 0)
  (setf *lastprime* 0)
  (loop for i from 2 while (< *primes* number)
	do (progn 
	     (if (primeBase i) (setq *primes* (+ 1 *primes*)))
	     (if (primeBase i) (setq *lastprime* i)) ) )
	
  (print *lastprime*))

(defun primeBase (number)
	(primeEx 2 number))

(defun primeEx (factorCandidate number)
	(cond ((> factorCandidate (sqrt number)) t) 
	      ((eq (mod number factorCandidate) 0) nil)
	      (t (primeEx (+ factorCandidate 1) number))))