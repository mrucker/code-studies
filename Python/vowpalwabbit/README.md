# Conda Environment
 + The PyPi package of [VowpalWabbit](https://pypi.org/project/vowpalwabbit/#files) includes C files which have to be built.
 + To make things easier a wheel package of vowpal was added to pypi with the C files pre-built for Windows.
 + The wheel packages target python 3.6 and 3.7. Therefore, conda environment.yml also targets python 3.7.