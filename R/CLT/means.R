
rpareto <- function(n) {
  return(exp(rexp(n)))
}

dpareto <- function(n) {
  if (n > 0) {
    return(dexp(log(n)))
  } else {
    return(0)
  }
}

rt2 <- function(n) {
  return(rt(n,2))
}

dt2 <- function(n) {
  return(dt(n,2))
}

plot_samples_stat <- function(r, stat, s=50:600) {
  plot(s, sapply(lapply(s, r), stat), 'l')
}

plot_samples_stat(dnorm  , function(x) x, s=seq(-4,4,by=.05))
plot_samples_stat(dunif  , function(x) x, s=seq(-4,4,by=.05))
plot_samples_stat(dlnorm , function(x) x, s=seq(-4,4,by=.05))
plot_samples_stat(dt2    , function(x) x, s=seq(-4,4,by=.05))
plot_samples_stat(dexp   , function(x) x, s=seq(-4,4,by=.05))
plot_samples_stat(dpareto, function(x) x, s=seq(-4,4,by=.05))

plot_samples_stat(rcauchy, mean) #undefined
plot_samples_stat(runif  , mean) #0.5
plot_samples_stat(rlnorm , mean) #1.6
plot_samples_stat(rt2    , mean) #0.0
plot_samples_stat(rexp   , mean) #1.0
plot_samples_stat(rpareto, mean) #infinite

plot_samples_stat(rcauchy, var) #undefined
plot_samples_stat(runif  , var) #finite
plot_samples_stat(rlnorm , var) #finite
plot_samples_stat(rt2    , var) #infinite
plot_samples_stat(rexp   , var) #finite
plot_samples_stat(rpareto, var) #infinite


