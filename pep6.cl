(defun p6 (number)
  (getDiff number))

(defun returnListOfNumbers (number)
  (if (eq number 0) '() (append (returnListOfNumbers (- number 1)) (cons number '())))) 

(defun sumOfSquares (numbers)
  (reduce #'+ (mapcar #'(lambda (x) (expt x 2)) numbers)))

(defun squareOfSum (numbers)
  (expt (reduce #'+ (mapcar #'(lambda (x) x) numbers)) 2))

(defun getDiff (number)
  (- (squareOfSum (returnListOfNumbers number)) (sumOfSquares (returnListOfNumbers number))))