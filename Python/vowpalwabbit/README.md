# Conda Environment
 + The PyPi package of [VowpalWabbit](https://pypi.org/project/vowpalwabbit/#files) includes C files which have to be built.
 + To make things easier for Windows users a platform wheel package of vowpal was added to pypi with the c files pre-built for Windows.
 + These pre-built packages were made to target python 3.6 and 3.7. Therefore, the conda environment targets python 3.7 with the latest pip version.