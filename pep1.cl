;;;   Chris Zimmerman
;;;   Project Euler problem 1
;;;   Objective: Find the sum of multiples of 3 and 5 under 1000.

; predicate which checks if number % modby is equal to zero
(defun modp (number modby)
  (if (eq (mod number modby) 0) t nil))

; loops from 1 to number and sums all numbers which satisfy either
; (or both) predicates
(defun findSumOfMults (number)
  (loop for i  
       below number
      when (or (modp i 3) (modp i 5)) sum i)) 

